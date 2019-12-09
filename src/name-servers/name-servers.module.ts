import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { NameServer } from '../models/name-server.entity';
import { NameServerController } from './name-servers.controller';
import { NameServersService } from './name-servers.service';

@Module({
	imports: [TypeOrmModule.forFeature([NameServer])],
	controllers: [NameServerController],
	providers: [NameServersService],
	exports: [NameServersService]
})
export class NameServersModule { }
