import { Controller, Post, Body, Get } from '@nestjs/common';
import { RegistrarsService } from './registrars.services';
import { CreateRegistrarDto } from './dto/create-registrar.dto';
import { Registrar } from 'src/models/registrar.entity';

@Controller('registrars')
export class RegistrarsController {
	constructor(private readonly registrarsService: RegistrarsService) {}

	@Post()
	async create(@Body() createRegistrarDto: CreateRegistrarDto) {
		await this.registrarsService.create(createRegistrarDto);
	}

	@Get()
	async findAll(): Promise<Registrar[]> {
		return await this.registrarsService.findAll();
	}
}
