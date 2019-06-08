import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { RepositoryService } from '@nestjsx/crud/typeorm';
import { Repository } from 'typeorm';
import { NameServer } from '../models';

@Injectable()
export class NameServersService extends RepositoryService<NameServer> {
	constructor(@InjectRepository(NameServer) repo: Repository<NameServer>) {
		super(repo);
	}
}
