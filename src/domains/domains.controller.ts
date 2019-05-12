import { Controller } from '@nestjs/common';
import { Crud } from '@nestjsx/crud';
import { Domain } from 'src/models/domain.entity';
import { DomainsService } from './domains.service';

@Crud(Domain)
@Controller('domains')
export class DomainsController {
	constructor(public service: DomainsService) { }
}
