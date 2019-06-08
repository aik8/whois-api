import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';

import { RegistrarsController } from './registrars.controller';
import { RegistrarsService } from './registrars.service';
import { Registrar } from '../models';

@Module({
	imports: [TypeOrmModule.forFeature([Registrar])],
	controllers: [RegistrarsController],
	providers: [RegistrarsService]
})
export class RegistrarsModule { }
