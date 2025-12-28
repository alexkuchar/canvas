import { Component, inject, signal, computed, effect } from '@angular/core';
import { Router, RouterOutlet, RouterLink, NavigationEnd } from '@angular/router';
import { filter } from 'rxjs/operators';
import { toSignal } from '@angular/core/rxjs-interop';
import { AuthService } from '../../../core/auth/auth.service';
import {
  MatNavList,
  MatListItem,
  MatListItemIcon,
  MatListItemTitle,
  MatDivider,
} from '@angular/material/list';
import { MatToolbar, MatToolbarRow } from '@angular/material/toolbar';
import { MatIconButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { MatMenu, MatMenuTrigger, MatMenuItem } from '@angular/material/menu';

@Component({
  selector: 'app-layout',
  imports: [
    RouterOutlet,
    RouterLink,
    MatNavList,
    MatListItem,
    MatListItemIcon,
    MatListItemTitle,
    MatToolbar,
    MatToolbarRow,
    MatIconButton,
    MatIcon,
    MatMenu,
    MatMenuTrigger,
    MatMenuItem,
    MatDivider,
  ],
  templateUrl: './layout.html',
  styleUrls: ['./layout.scss'],
})
export class Layout {
  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);

  readonly user = toSignal(this.authService.user$, { initialValue: this.authService.user });
  readonly currentUrl = signal(this.router.url);
  readonly userMenuOpen = signal(false);

  readonly userDisplayName = computed(() => {
    const currentUser = this.user();
    if (!currentUser) return '';
    const { firstName, lastName } = currentUser.user;
    return `${firstName} ${lastName}`.trim() || currentUser.user.email;
  });

  readonly menuItems = [
    {
      group: 'Account',
      items: [
        {
          label: 'Settings',
          icon: 'settings',
          action: () => this.openSettings(),
        },
        {
          label: 'Logout',
          icon: 'logout',
          action: () => this.logout(),
        },
      ],
    },
  ];

  readonly sidebarItems = [
    {
      label: 'Home',
      icon: 'home',
      route: '/dashboard',
    },
    {
      label: 'Boards',
      icon: 'dashboard',
      route: '/dashboard/boards',
    },
  ];

  constructor() {
    this.router.events
      .pipe(filter((event) => event instanceof NavigationEnd))
      .subscribe((event) => {
        if (event instanceof NavigationEnd) {
          this.currentUrl.set(event.urlAfterRedirects);
        }
      });
  }

  isActiveRoute(route: string): boolean {
    return this.currentUrl() === route;
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }

  openSettings() {
    this.router.navigate(['/dashboard/settings']);
  }
}
