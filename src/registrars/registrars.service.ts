import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { RepositoryService } from '@nestjsx/crud/typeorm';
import { Repository } from 'typeorm';
import { Registrar } from 'src/models/registrar.entity';

@Injectable()
export class RegistrarsService extends RepositoryService<Registrar> {
	constructor(@InjectRepository(Registrar) repo: Repository<Registrar>) {
		super(repo);
	}
}
