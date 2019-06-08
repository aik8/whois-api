import { Controller, Get } from '@nestjs/common';
import { Crud } from '@nestjsx/crud';
import { Registrar } from '../models';
import { RegistrarsService } from './registrars.service';

@Crud(Registrar)
@Controller('registrars')
export class RegistrarsController {
	constructor(public service: RegistrarsService) { }
}
