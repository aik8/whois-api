import { Table, Model } from 'sequelize-typescript';

@Table({
	tableName: 'snapshot_nameservers'
})
export class SnapshotNameservers extends Model<SnapshotNameservers> { }
