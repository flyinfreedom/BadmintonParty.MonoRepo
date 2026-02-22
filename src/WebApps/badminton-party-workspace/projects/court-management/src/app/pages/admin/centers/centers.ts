import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatTooltipModule } from '@angular/material/tooltip';
import { CenterDialogComponent } from './center-dialog/center-dialog';
import { CenterSettingsDialogComponent } from './settings-dialog/settings-dialog';

export interface BadmintonCenter {
    id: string;
    name: string;
    description?: string;
    owner?: string;
    address?: string;
    phone?: string;
    lineAccount?: string;
    settings?: CenterSettings;
}

export interface CenterSettings {
    bookingRangeDays?: number;
    unavailableHours?: number[];
    peakSettings: {
        dayOfWeek: number;
        peakHours: number[];
    }[];
    normalFee?: number;
    peakFee?: number;
}

@Component({
    selector: 'app-centers',
    standalone: true,
    imports: [
        CommonModule,
        MatTableModule,
        MatButtonModule,
        MatIconModule,
        MatCardModule,
        MatDialogModule,
        MatTooltipModule
    ],
    templateUrl: './centers.html',
    styleUrls: ['./centers.scss']
})
export class CentersComponent {
    displayedColumns: string[] = ['name', 'owner', 'phone', 'address', 'settings', 'actions'];

    centers: BadmintonCenter[] = [
        {
            id: '1',
            name: '板橋羽球館',
            owner: '王小明',
            phone: '02-1234-5678',
            address: '新北市板橋區中正路1號',
            lineAccount: '@bjbc'
        },
        {
            id: '2',
            name: '大安羽球中心',
            owner: '李四',
            phone: '02-8765-4321',
            address: '台北市大安區新生南路二段',
            lineAccount: '@daan_badminton'
        }
    ];

    constructor(private dialog: MatDialog) { }

    addCenter() {
        const dialogRef = this.dialog.open(CenterDialogComponent, {
            data: {}
        });

        dialogRef.afterClosed().subscribe(result => {
            if (result) {
                const newCenter = { ...result, id: (this.centers.length + 1).toString() };
                this.centers = [...this.centers, newCenter];
            }
        });
    }

    editCenter(center: BadmintonCenter) {
        const dialogRef = this.dialog.open(CenterDialogComponent, {
            data: { center }
        });

        dialogRef.afterClosed().subscribe(result => {
            if (result) {
                this.centers = this.centers.map(c => c.id === result.id ? result : c);
            }
        });
    }

    deleteCenter(center: BadmintonCenter) {
        if (confirm(`確定要刪除 ${center.name} 嗎？`)) {
            this.centers = this.centers.filter(c => c.id !== center.id);
        }
    }

    openSettings(center: BadmintonCenter) {
        const dialogRef = this.dialog.open(CenterSettingsDialogComponent, {
            data: { center },
            width: '650px'
        });

        dialogRef.afterClosed().subscribe(result => {
            if (result) {
                // Update center with new settings
                this.centers = this.centers.map(c =>
                    c.id === center.id ? { ...c, settings: result } : c
                );
            }
        });
    }
}
