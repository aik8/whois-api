import { Injectable } from '@nestjs/common';
import * as axios from 'axios';
import * as jsdom from 'jsdom';
import * as _ from 'lodash';
import * as moment from 'moment';
import { URL } from 'url';
import { Domain } from '../models/domain.entity';
import { NameServer } from '../models/name-server.entity';
import { Registrar } from '../models/registrar.entity';
import { PiosResult } from './pios-result';

@Injectable()
export class PiosService {
	query(domain: string): Promise<PiosResult | null> {
		const url = new URL(`https://grwhois.ics.forth.gr:800/plainwhois/plainWhois?domainName=${domain}`);
		return axios.default.get(url.href).then(this.parseResponse);
	}

	private parseResponse(response: axios.AxiosResponse<any>): PiosResult | null {
		// Parse the HTML in the response.
		const dom = new jsdom.JSDOM(response.data);

		// Get the contents of body, removing all new line chars.
		const html = _.replace(dom.window.document.body.innerHTML, /\n/gm, '');
		let result: PiosResult = {
			domain: new Domain(),
			registrar: new Registrar(),
			nameServers: new Array<NameServer>()
		};

		// If the query returned a result, it will contain <br> elements.
		if (/.*?(?=<br>)/gm.test(html)) {
			// Remove all new line chars and <br> elements, replaced by commas.
			const filtered = _.split(_.replace(html, /<br>/gm, ','), ',');
			// The last element of the resulting array is always an empty string.
			filtered.pop();

			// Start forming the result, which will be a JSON object.
			const fields = _.map(filtered, row => {
				const field = row.split(':');
				const key = field[0];

				// Some fields contain URLs, so they were split multiple times.
				// This will stitch them back together.
				const value = _.join(_.tail(field), ':');

				return { key, value };
			});

			// Parse the dates properly for JavaScript.
			const creation = moment(fields[3].value, 'YYYY-MM-DD').toDate();
			const expiration = moment(fields[4].value, 'YYYY-MM-DD').toDate();
			const updated = moment(fields[5].value, 'YYYY-MM-DD').toDate();

			// Just fill all the fields in the result with the values above.

			/* Domain */
			result.domain.name = fields[0].value;      // Domain Name
			result.domain.handle = fields[1].value;    // Domain Handle
			result.domain.creation = creation;         // Creation Date
			result.domain.expiration = expiration;     // Expiration Date
			result.domain.last_update = updated;       // Updated Date

			/* Current Registrar */
			result.registrar.name = fields[6].value;   // Registrar
			result.registrar.url = fields[7].value;    // Registrar Referral URL
			result.registrar.email = fields[8].value;  // Registrar Email
			result.registrar.phone = fields[9].value;  // Registrar Telephone

			/* Name Servers */
			for (let i = 12; i < fields.length; i ++) {
				const j = result.nameServers.push(new NameServer());
				result.nameServers[j - 1].name = fields[i].value;
			}
		} else {
			// In case the request was unsuccessful the result will only
			// contain the error message. Thus the result will be null.
			result = null;
		}

		return result;
	}
}
