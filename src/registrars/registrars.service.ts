import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { RepositoryService } from '@nestjsx/crud/typeorm';
import { Repository } from 'typeorm';
import { Registrar } from '../models';

@Injectable()
export class RegistrarsService extends RepositoryService<Registrar> {
	constructor(@InjectRepository(Registrar) registrars: Repository<Registrar>) {
		super(registrars);
	}
}
