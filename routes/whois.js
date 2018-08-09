const URL = require('url').URL;
const express = require('express');
const router = express.Router();
const axios = require('axios');
const jsdom = require('jsdom');
const JSDOM = jsdom.JSDOM;
const _ = require('lodash');

router.get('/', (req, res, next) => {
	const url = new URL(`https://grwhois.ics.forth.gr:800/plainwhois/plainWhois?domainName=${req.query.domain}`)
	axios.get(url.href)
		.then(response => response.data) // Extract the data from the response.
		.then(response => new JSDOM(response)) // Parse the HTML in the response.
		.then(dom => {
			// Get the contents of the body, removing all new line chars.
			let result = _.replace(dom.window.document.body.innerHTML, /\n/gm, '');
			let response = {};

			// If the query returned a result, it will contain <br> elements.
			if (/.*?(?=<br>)/gm.test(result)) {
				// Remove all new line chars and <br> elements, replaced by commas.
				let filtered = _.split(_.replace(result, /<br>/gm, ','), ',');
				// The last element of the resulting array is always an empty string.
				filtered.pop();

				// Start forming the response, which will be a JSON object.
				let fields = _.map(filtered, row => {
					let field = row.split(':');
					let key = field[0];

					// Some fields contain URLs, so they were split multiple
					// times. This will stitch them back together.
					let value = _.join(_.tail(field), ':');

					// Add some stuff to be used by the template.
					let id = _.snakeCase(_.lowerCase(key));
					let htmlClass = _.kebabCase(_.lowerCase(key));

					return { id, htmlClass, key, value };
				});
				response.fields = fields;

				// Add the "result" property, to fill in the appropriate GUI element.
				response.result = 'Domain already registered!';
				response.success = true;
			} else {
				// In case the request was unsuccessful the result will only
				// contain the error message. Thus the response's "result"
				// property will is the best to accommodate it.
				response = { result, success: false };
			}
			
			res.render('index', { title: `Αποτελέσματα για το ${req.query.domain}`, response });
		})
		.catch(next);
});

module.exports = router;
