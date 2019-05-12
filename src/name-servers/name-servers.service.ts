import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';

import { NameServer } from '../models/name-server.entity';
import { NameServerDto } from './dto/name-server.dto';


@Injectable()
export class NameServersService {
	constructor(
		@InjectRepository(NameServer)
		private readonly nameServerRepository: Repository<NameServer>
	) { }

	findAll(): Promise<NameServer[]> {
		return this.nameServerRepository.find();
	}

	create(nameServer: NameServerDto): Promise<NameServer> {
		const ns = this.nameServerRepository.create(nameServer);
		return this.nameServerRepository.save(ns);
	}
}
