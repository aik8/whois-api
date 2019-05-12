import { Controller, Post, Body, Get } from '@nestjs/common';
import { NameServersService } from './name-servers.service';
import { NameServer } from '../models/name-server.entity';
import { NameServerDto } from './dto/name-server.dto';

@Controller('ns')
export class NameServerController {
	constructor(private readonly nsService: NameServersService) { }

	@Get()
	async findAll(): Promise<NameServer[]> {
		return await this.nsService.findAll();
	}
}
