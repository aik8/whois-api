import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { TypeOrmCrudService } from '@nestjsx/crud-typeorm';
import { Repository } from 'typeorm';
import { Domain } from '../models';
import { IDomainCreateDto } from './dtos/domain-create.dto';

@Injectable()
export class DomainsService extends TypeOrmCrudService<Domain> {
	constructor(@InjectRepository(Domain) repo: Repository<Domain>) {
		super(repo);
	}

	public save(domain: IDomainCreateDto): Promise<Domain> {
		return this.repo.save(this.repo.create(domain));
	}
}
