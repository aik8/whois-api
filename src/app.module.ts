import { Module } from '@nestjs/common';
import { AppController } from './app.controller';
import { AppService } from './app.service';
import { DomainsModule } from './models/domains/domains.module';

@Module({
	imports: [DomainsModule],
	controllers: [AppController],
	providers: [AppService],
})
export class AppModule { }
