import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';

import { NameServer } from '../models/name-server.entity';
import { NameServerDto } from 'dist/src/ns/dto/ns.dto';

@Injectable()
export class NameServersService {
	constructor(
		@InjectRepository(NameServer)
		private readonly nameServerRepository: Repository<NameServer>
	) { }

	findAll(): Promise<NameServer[]> {
		return this.nameServerRepository.find();
	}
}
