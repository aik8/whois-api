import { Injectable, Inject } from '@nestjs/common';
import { Domain } from '../models/domain.entity';
import { CreateDomainDto } from './dto/create-domain.dto';

@Injectable()
export class DomainsService {
	constructor(
		@Inject('DOMAINS_REPOSITORY') private readonly domainsRepository: typeof Domain
	) {}

	async create(createDomainDto: CreateDomainDto): Promise<Domain> {
		const domain = new Domain();
		domain.name = createDomainDto.name;

		return await domain.save();
	}

	async findAll(): Promise<Domain[]> {
		return await this.domainsRepository.findAll<Domain>();
	}
}
