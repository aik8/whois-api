import { Controller, Get } from '@nestjs/common';
import { Domain } from '../models/domain.entity';
import { DomainsService } from './domains.service';

@Controller('domains')
export class DomainsController {
	constructor(private readonly domainsService: DomainsService) { }

	@Get()
	async findAll(): Promise<Domain[]> {
		return this.domainsService.findAll();
	}
}
