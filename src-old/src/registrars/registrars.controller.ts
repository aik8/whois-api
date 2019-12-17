import { Controller } from '@nestjs/common';
import { ApiUseTags } from '@nestjs/swagger';
import { Crud, CrudController } from '@nestjsx/crud';
import { Registrar } from '../models';
import { RegistrarsService } from './registrars.service';

@Crud({
	model: {
		type: Registrar
	}
})
@ApiUseTags('registrars')
@Controller('registrars')
export class RegistrarsController implements CrudController<Registrar> {
	constructor(public service: RegistrarsService) { }
}
