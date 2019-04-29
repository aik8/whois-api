import { Module } from '@nestjs/common';
import { DatabaseModule } from 'src/database/database.module';
import { DomainsController } from './domains.controller';
import { DomainsService } from './domains.service';
import { domainsProviders } from './domains.provider';

@Module({
	imports: [DatabaseModule],
	controllers: [DomainsController],
	providers: [DomainsService, ...domainsProviders]
})
export class DomainsModule { }
