import { Controller, Post, Body, Get } from '@nestjs/common';
import { NameServersService } from './ns.service';
import { NameServerDto } from './dto/ns.dto';
import { NameServer } from './ns.entity';

@Controller('ns')
export class NameServerController {
	constructor(private readonly nsService: NameServersService) { }

	@Post()
	async create(@Body() nsDto: NameServerDto) {
		await this.nsService.create(nsDto);
	}

	@Get()
	async findAll(): Promise<NameServer[]> {
		return await this.nsService.findAll();
	}
}
