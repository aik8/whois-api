import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { RestfulOptions } from '@nestjsx/crud';
import { RepositoryService } from '@nestjsx/crud/typeorm';
import { Domain } from '../models';
import { Repository } from 'typeorm';

@Injectable()
export class DomainsService extends RepositoryService<Domain> {
	protected options: RestfulOptions = {
		join: {
			snapshots: {}
		}
	};

	constructor(@InjectRepository(Domain) repo: Repository<Domain>) {
		super(repo);
	}
}
