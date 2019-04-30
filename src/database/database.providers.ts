import { Sequelize } from 'sequelize-typescript';
import { Domain } from '../models/domain.entity';
import { Snapshot } from 'src/models/snapshot.entity';
import { Registrar } from 'src/models/registrar.entity';
import { NameServer } from 'src/models/ns.entity';
import { SnapshotNameservers } from 'src/models/snapshot-ns.entity';

export const databaseProviders = [
	{
		provide: 'SEQUELIZE',
		useFactory: async () => {
			const sequelize = new Sequelize('whois', 'whois', 'siohw', {
				dialect: 'mariadb'
			});

			sequelize.addModels([
				Domain,
				NameServer,
				Registrar,
				Snapshot,
				SnapshotNameservers
			]);

			Domain.hasMany(Snapshot, { as: 'snapshots' });
			Snapshot.hasOne(Registrar, { as: 'registrar' });

			Snapshot.belongsToMany(NameServer, { through: { model: SnapshotNameservers } });
			NameServer.belongsToMany(Snapshot, { through: SnapshotNameservers });

			await sequelize.sync();
			return sequelize;
		}
	}
];
