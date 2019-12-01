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

	public save(ns: INameServerCreateDto): Promise<NameServer> {
		return this.repo.save(this.repo.create(ns));
	}
}
