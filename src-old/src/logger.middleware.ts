import { Injectable, NestMiddleware } from '@nestjs/common';
import * as chalk from 'chalk';
import * as _ from 'lodash';
import * as moment from 'moment';

@Injectable()
export class LoggerMiddleware implements NestMiddleware {
	use(req: Request, res: Response, next: Function) {
		this.printInfo(_.get(req.headers, 'x-real-ip', 'unkown?!'), req.method, req.url);
		next();
	}

	private format = 'YYYY-MM-DD hh:mm:ss A';

	private printTimestamp() {
		const now = moment().format(this.format);
		process.stdout.write(`[${chalk.gray.bold(now)}] `);
	}

	private printLogInfo(ip: string, method: string, route: string) {
		process.stdout.write(`IP: ${chalk.yellow(ip)} | `);
		process.stdout.write(`Method: ${chalk.magenta(method)} | `);
		process.stdout.write(`Route: ${chalk.green(route)} `);
	}

	private printMsg(msg: string) {
		process.stdout.write(`:: Message: ${msg}`);
		process.stdout.write('\n');
	}

	private printInfo(ip: string, method: string, route: string, msg?: string) {
		process.stdout.write(`[${chalk.bold.blueBright('INFO')}] `);
		this.printTimestamp();
		this.printLogInfo(ip, method, route);

		if (msg) {
			this.printMsg(msg);
		} else {
			process.stdout.write('\n');
		}
	}
}
