import { Controller, Post, Body, Get } from '@nestjs/common';
import { DomainsService } from './domains.service';
import { CreateDomainDto } from './dto/create-domain.dto';
import { Domain } from '../models/domain.entity';

@Controller('domains')
export class DomainsController {
	constructor(private readonly domainsService: DomainsService) { }

	@Post()
	async create(@Body() createDomainDto: CreateDomainDto) {
		await this.domainsService.create(createDomainDto);
	}

	@Get()
	async findAll(): Promise<Domain[]> {
		return await this.domainsService.findAll();
	}
}
