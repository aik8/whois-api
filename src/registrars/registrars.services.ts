import { Injectable } from '@nestjs/common';
import { Repository } from 'typeorm';
import { InjectRepository } from '@nestjs/typeorm';

import { Registrar } from '../models/registrar.entity';
import { CreateRegistrarDto } from './dto/create-registrar.dto';

@Injectable()
export class RegistrarsService {
	constructor(
		@InjectRepository(Registrar)
		private readonly registrarRepository: Repository<Registrar>
	) { }

	findAll(): Promise<Registrar[]> {
		return this.registrarRepository.find();
	}

	create(createRegistrarDto: CreateRegistrarDto): Promise<Registrar> {
		const registrar = this.registrarRepository.create(createRegistrarDto);
		return this.registrarRepository.save(registrar);
	}
}
