import { Controller } from '@nestjs/common';
import { ApiUseTags } from '@nestjs/swagger';
import { Crud } from '@nestjsx/crud';
import { Domain } from '../models';
import { DomainsService } from './domains.service';

@Crud({
	model: {
		type: Domain
	},
	query: {
		join: {
			snapshots: {}
		}
	}
})
@ApiUseTags('domains')
@Controller('domains')
export class DomainsController {
	constructor(public service: DomainsService) { }
}
