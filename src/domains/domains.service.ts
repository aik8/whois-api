import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { RepositoryService } from '@nestjsx/crud/typeorm';
import { Repository } from 'typeorm';
import { Domain } from 'src/models/domain.entity';

@Injectable()
export class DomainsService extends RepositoryService<Domain> {
	constructor(@InjectRepository(Domain) repo: Repository<Domain>) {
		super(repo);
	}
}
