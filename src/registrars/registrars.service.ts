import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { TypeOrmCrudService } from '@nestjsx/crud-typeorm';
import { Repository } from 'typeorm';
import { IRegistrarCreateDto } from '../interfaces';
import { Registrar } from '../models';

@Injectable()
export class RegistrarsService extends TypeOrmCrudService<Registrar> {
	constructor(@InjectRepository(Registrar) registrars: Repository<Registrar>) {
		super(registrars);
	}

	public findOrInsert = async (registrar: Partial<Registrar> | IRegistrarCreateDto): Promise<Registrar> => {
		// Check if the registrar exists and return it, if it does.
		const existing = await this.repo.findOne({ name: registrar.name });
		if (existing) return existing;

		// At this point it is clear that such a registrar does not exist.
		const newRegistrar = this.repo.create(registrar);

		// Return the operation result (a Registrar).
		return this.repo.save(newRegistrar);
	}
}
