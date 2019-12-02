import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { TypeOrmCrudService } from '@nestjsx/crud-typeorm';
import { Repository } from 'typeorm';
import { INameServerCreateDto } from '../interfaces';
import { NameServer } from '../models';

@Injectable()
export class NameServersService extends TypeOrmCrudService<NameServer> {
	constructor(@InjectRepository(NameServer) repo: Repository<NameServer>) {
		super(repo);
	}

	public findOrInsert = async (ns: Partial<NameServer> | INameServerCreateDto): Promise<NameServer> => {
		// Check if the domain exists and return it, if it does.
		const existing = await this.repo.findOne(ns);
		if (existing) return existing;

		// At this point it is clear that such a NameServer does not exist.
		const newNameServer = this.repo.create(ns);

		// Return the operation result (a NameServer).
		return this.repo.save(newNameServer);
	}
}
