import { Module } from '@nestjs/common';
import { AppController } from './app.controller';
import { AppService } from './app.service';
import { DomainsModule } from './models/domains/domains.module';
import { NameServersModule } from './models/ns/ns.module';
import { RegistrarsModule } from './models/registrars/registrars.module';

@Module({
	imports: [DomainsModule, NameServersModule, RegistrarsModule],
	controllers: [AppController],
	providers: [AppService],
})
export class AppModule { }
