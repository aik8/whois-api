import { Module } from '@nestjs/common';
import { DatabaseModule } from 'src/database/database.module';
import { RegistrarsController } from './registrars.controller';
import { RegistrarsService } from './registrars.services';
import { registrarProviders } from './registrars.provider';

@Module({
	imports: [DatabaseModule],
	controllers: [RegistrarsController],
	providers: [RegistrarsService, ...registrarProviders]
})
export class RegistrarsModule { }
