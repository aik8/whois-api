import { Controller } from '@nestjs/common';
import { ApiUseTags } from '@nestjs/swagger';
import { Crud } from '@nestjsx/crud';
import { Registrar } from '../models';
import { RegistrarsService } from './registrars.service';

@Crud({
	model: {
		type: Registrar
	}
})
@ApiUseTags('registrars')
@Controller('registrars')
export class RegistrarsController {
	constructor(public service: RegistrarsService) { }
}
