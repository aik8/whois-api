import { Injectable, Inject } from '@nestjs/common';
import { NameServer } from './ns.entity';
import { NameServerDto } from './dto/ns.dto';

@Injectable()
export class NameServersService {
	constructor(@Inject('NAMESERVERS_REPOSITORY') private readonly nsRepository: typeof NameServer) { }

	async create(nsDto: NameServerDto): Promise<NameServer> {
		const ns = new NameServer();
		ns.name = nsDto.name;

		return await ns.save();
	}

	async findAll(): Promise<NameServer[]> {
		return await this.nsRepository.findAll<NameServer>();
	}
}
