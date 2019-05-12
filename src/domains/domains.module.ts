import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { Domain } from '../models/domain.entity';
import { DomainsController } from './domains.controller';
import { DomainsService } from './domains.service';

@Module({
	imports: [TypeOrmModule.forFeature([Domain])],
	controllers: [DomainsController],
	providers: [DomainsService]
})
export class DomainsModule { }
