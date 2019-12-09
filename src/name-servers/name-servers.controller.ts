import { Controller } from '@nestjs/common';
import { ApiUseTags } from '@nestjs/swagger';
import { Crud } from '@nestjsx/crud';
import { NameServer } from '../models/name-server.entity';
import { NameServersService } from './name-servers.service';

@Crud({
	model: {
		type: NameServer
	}
})
@ApiUseTags('ns')
@Controller('ns')
export class NameServerController {
	constructor(public service: NameServersService) { }
}
