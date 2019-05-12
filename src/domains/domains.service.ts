import { Inject, Injectable } from '@nestjs/common';
import { Repository } from 'typeorm';
import { Domain } from '../models/domain.entity';
import { InjectRepository } from '@nestjs/typeorm';

@Injectable()
export class DomainsService {
	constructor(
		@InjectRepository(Domain)
		private readonly domainsRepository: Repository<Domain>
	) { }

	findAll(): Promise<Domain[]> {
		return this.domainsRepository.find();
	}
}
