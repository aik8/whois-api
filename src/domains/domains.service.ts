import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { TypeOrmCrudService } from '@nestjsx/crud-typeorm';
import { Repository } from 'typeorm';
import { IDomainCreateDto } from '../interfaces';
import { Domain } from '../models';

@Injectable()
export class DomainsService extends TypeOrmCrudService<Domain> {
	constructor(@InjectRepository(Domain) repo: Repository<Domain>) {
		super(repo);
	}

	public findOrInsert = async (domain: Partial<Domain> | IDomainCreateDto): Promise<Domain> => {
		// Check if the domain exists and return it, if it does.
		const existing = await this.repo.findOne({
			name: domain.name,
			handle: domain.handle
		});

		if (existing) return existing;

		// At this ponit it is clear that such a domain does not exist.
		const newDomain = this.repo.create(domain);

		// Return the operation result (a Domain).
		return this.repo.save(newDomain);
	}
}
