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

	public save(registrar: IRegistrarCreateDto): Promise<Registrar> {
		return this.repo.save(this.repo.create(registrar));
	}
}
