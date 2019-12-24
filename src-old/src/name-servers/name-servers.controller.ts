import { Controller } from '@nestjs/common';
import { ApiUseTags } from '@nestjs/swagger';
import { Crud, CrudController } from '@nestjsx/crud';
import { NameServer } from '../models/name-server.entity';
import { NameServersService } from './name-servers.service';

@Crud({
	model: {
		type: NameServer
	}
})
@ApiUseTags('ns')
@Controller('ns')
export class NameServerController implements CrudController<NameServer> {
	constructor(public service: NameServersService) { }
}
