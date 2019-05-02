import { Injectable, Inject } from '@nestjs/common';
import { Registrar } from '../models/registrar.entity';
import { CreateRegistrarDto } from './dto/create-registrar.dto';

@Injectable()
export class RegistrarsService {
	constructor(@Inject('REGISTRARS_REPOSITORY') private readonly registrarsRepository: typeof Registrar) { }

	async create(createRegistrarDto: CreateRegistrarDto): Promise<Registrar> {
		const registrar = new Registrar();
		registrar.name = createRegistrarDto.name;

		return await registrar.save();
	}

	async findAll(): Promise<Registrar[]> {
		return await this.registrarsRepository.findAll<Registrar>();
	}
}
