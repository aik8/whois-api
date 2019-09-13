import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { TypeOrmCrudService } from '@nestjsx/crud-typeorm';
import { Repository } from 'typeorm';
import { DomainsService } from '../domains/domains.service';
import { NameServer, Snapshot } from '../models';
import { NameServersService } from '../name-servers/name-servers.service';
import { IPiosResult } from '../pios/pios-result';
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
		// Check if the domain already exists in the database.
		// Create it, if necessary.
		let domain = await this.domains.findOne(result.domain);
		domain = !domain ? await this.domains.save(result.domain) : domain;

		// If the domain is registered, do the same for the registrar...
		let registrar = !domain.registered ? null : await this.registrars.findOne(result.registrar);
		registrar = domain.registered && !registrar ? await this.registrars.save(result.registrar) : registrar;

		// ... and for the name servers.
		const nameServers: NameServer[] = domain.registered ? [] : null;
		if (domain.registered && result.nameServers) {
			for (const ns of result.nameServers) {
				let nameServer = await this.nameServers.findOne(ns);
				nameServer = !nameServer ? await this.nameServers.save(ns) : nameServer;
				nameServers.push(nameServer);
			}
		}

		// Finally create the snapshot and save it in the database.
		return this.repo.save(this.repo.create({
			domain, registrar, nameServers, registered: domain.registered
		}));
	}
}
