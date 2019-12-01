import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { Registrar } from '../models';
import { RegistrarsController } from './registrars.controller';
import { RegistrarsService } from './registrars.service';

@Module({
	imports: [TypeOrmModule.forFeature([Registrar])],
	controllers: [RegistrarsController],
	providers: [RegistrarsService],
	exports: [RegistrarsService]
})
export class RegistrarsModule { }
