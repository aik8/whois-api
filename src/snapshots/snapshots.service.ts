import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { RepositoryService } from '@nestjsx/crud/typeorm';
import { Repository } from 'typeorm';
import { Snapshot, Domain, Registrar, NameServer } from '../models';
import { RestfulOptions } from '@nestjsx/crud';
import { IPiosResult } from '../pios/pios-result';

@Injectable()
export class SnapshotsService extends RepositoryService<Snapshot> {
	protected options: RestfulOptions = {
		join: {
			domain: {},
			registrar: {},
			nameServers: {}
		}
	};

	constructor(
		@InjectRepository(Snapshot) private snapshots: Repository<Snapshot>,
		@InjectRepository(Domain) private domains: Repository<Domain>,
		@InjectRepository(Registrar) private registrars: Repository<Registrar>,
		@InjectRepository(NameServer) private nameServers: Repository<NameServer>
	) {
		super(snapshots);
	}

	public createSnapshot = async (result: IPiosResult): Promise<Snapshot> => {
		// Check if the domain already exists in the database.
		// Create it, if necessary.
		let domain = await this.domains.findOne(result.domain);
		domain = !domain ? await this.domains.save(this.domains.create(result.domain)) : domain;

		// If the domain is registered, do the same for the registrar...
		let registrar = !domain.registered ? null : await this.registrars.findOne(result.registrar);
		registrar = domain.registered && !registrar ? await this.registrars.save(this.registrars.create(result.registrar)) : registrar;

		// ... and for the name servers.
		const nameServers: NameServer[] = domain.registered ? [] : null;
		if (domain.registered && result.nameServers) {
			for (let ns of result.nameServers) {
				let nameServer = await this.nameServers.findOne(ns);
				nameServer = !nameServer ? await this.nameServers.save(this.nameServers.create(ns)) : nameServer;
				nameServers.push(nameServer);
			}
		}

		// Finally create the snapshot and save it in the database.
		return this.snapshots.save(this.snapshots.create({
			domain, registrar, nameServers, registered: domain.registered
		}));
	}
}
