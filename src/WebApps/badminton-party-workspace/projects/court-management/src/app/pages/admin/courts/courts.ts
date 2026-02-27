import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatChipsModule } from '@angular/material/chips';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatDividerModule } from '@angular/material/divider';
import { CourtDialogComponent } from './court-dialog/court-dialog';

export interface BadmintonCourt {
    id: string;
    centerId: string;
    centerName: string;
    name: string;
    description?: string;
    normalFee?: number;
    peakFee?: number;
    disabledPeriods?: string;
    status: 'available' | 'maintenance' | 'closed';
}

@Component({
    selector: 'app-courts',
    standalone: true,
    imports: [
        CommonModule,
        MatTableModule,
        MatButtonModule,
        MatIconModule,
        MatCardModule,
        MatSelectModule,
        MatFormFieldModule,
        MatChipsModule,
        MatTooltipModule,
        MatDialogModule,
        MatDividerModule
    ],

    templateUrl: './courts.html',
    styleUrls: ['./courts.scss']
})
export class CourtsComponent {
    displayedColumns: string[] = ['name', 'center', 'fees', 'disabled', 'status', 'actions'];

    centers = [
        { id: '1', name: '板橋羽球館' },
        { id: '2', name: '大安羽球中心' }
    ];

    courts: BadmintonCourt[] = [
        {
            id: 'c1',
            centerId: '1',
            centerName: '板橋羽球館',
            name: 'A 場',
            description: '靠窗側',
            normalFee: 300,
            peakFee: 500,
            disabledPeriods: '週一 08:00-10:00',
            status: 'available'
        },
        {
            id: 'c2',
            centerId: '1',
            centerName: '板橋羽球館',
            name: 'B 場',
            normalFee: 300,
            peakFee: 500,
            status: 'maintenance'
        }
    ];

    constructor(private dialog: MatDialog) { }

    addCourt() {
        const dialogRef = this.dialog.open(CourtDialogComponent, {
            data: { centers: this.centers }
        });

        dialogRef.afterClosed().subscribe(result => {
            if (result) {
                const newCourt = { ...result, id: 'c' + (this.courts.length + 1) };
                this.courts = [...this.courts, newCourt];
            }
        });
    }

    editCourt(court: BadmintonCourt) {
        const dialogRef = this.dialog.open(CourtDialogComponent, {
            data: { court, centers: this.centers }
        });

        dialogRef.afterClosed().subscribe(result => {
            if (result) {
                this.courts = this.courts.map(c => c.id === result.id ? result : c);
            }
        });
    }

    deleteCourt(court: BadmintonCourt) {
        if (confirm(`確定要刪除 ${court.name} 嗎？`)) {
            this.courts = this.courts.filter(c => c.id !== court.id);
        }
    }

    getStatusLabel(status: string): string {
        switch (status) {
            case 'available': return '正常';
            case 'maintenance': return '維護中';
            case 'closed': return '停用';
            default: return '未知';
        }
    }

    getStatusClass(status: string): string {
        return `status-badge ${status}`;
    }
}
