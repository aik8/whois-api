import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { TypeOrmCrudService } from '@nestjsx/crud-typeorm';
import { Repository } from 'typeorm';
import { DomainsService } from '../domains/domains.service';
import { IPiosResult } from '../interfaces';
import { NameServer, Registrar, Snapshot } from '../models';
import { NameServersService } from '../name-servers/name-servers.service';
import { RegistrarsService } from '../registrars/registrars.service';

@Injectable()
export class SnapshotsService extends TypeOrmCrudService<Snapshot> {
	constructor(
		@InjectRepository(Snapshot) public repo: Repository<Snapshot>,
		private domains: DomainsService,
		private registrars: RegistrarsService,
		private nameServers: NameServersService
	) {
		super(repo);
	}

	public createSnapshot = async (result: IPiosResult): Promise<Snapshot> => {
		// Check if the domain already exists in the database and
		// create it, if necessary.
		const domain = await this.domains.findOrInsert(result.domain);

		// If the domain is registered, do the same for the registrar...
		let registrar: Registrar | null = null;
		if (domain.registered && result.registrar) {
			registrar = await this.registrars.findOrInsert(result.registrar);
		}

		// ... and for the name servers.
		let nameServers: NameServer[] | null = null;
		if (domain.registered && result.nameServers) {
			// The domain is registered and it already has name servers.
			nameServers = [];

			// Iterate through the list and build the NameServer array.
			for (const ns of result.nameServers) {
				const nameServer = await this.nameServers.findOrInsert(ns);
				nameServers.push(nameServer);
			}
		}

		// Finally create the snapshot and save it in the database.
		return this.repo.save(this.repo.create({
			domain,
			registrar,
			nameServers,
			registered: domain.registered
		} as Snapshot));
	}
}
