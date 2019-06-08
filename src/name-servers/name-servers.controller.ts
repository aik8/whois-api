import { Controller } from '@nestjs/common';
import { Crud } from '@nestjsx/crud';
import { NameServer } from '../models/name-server.entity';
import { NameServersService } from './name-servers.service';

@Crud(NameServer)
@Controller('ns')
export class NameServerController {
	constructor(public service: NameServersService) { }
}
