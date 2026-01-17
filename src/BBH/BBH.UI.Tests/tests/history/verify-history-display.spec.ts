// spec: test-plan.md
// seed: tests/seed.spec.ts

import { test, expect } from '@playwright/test';

test.describe('History Feature - Basic Functionality', () => {
  test('Verify History Page Displays All Visited Pages with Visit Counts', async ({ page }) => {
    await page.goto('http://localhost:5023/');

    await page.waitForLoadState('networkidle');
    await page.waitForLoadState('load');
    await page.waitForLoadState('domcontentloaded');
    
    // Navigate to http://localhost:5023/
    // Click on Counter link in the navigation menu
    await page.getByRole('navigation').getByRole('link', { name: 'Counter' }).click();

    // Click on Weather link in the navigation menu
    await page.getByRole('navigation').getByRole('link', { name: 'Weather' }).click();

    // Click on Home link in the navigation menu
    await page.getByRole('link', { name: 'Home' }).click();

    // Click on History link in the navigation menu
    await page.getByRole('navigation').getByRole('link', { name: 'History' }).click();

    // Observe the History page table displaying all visited pages
    await expect(page.getByText('Number of history items 3')).toBeVisible();
    await expect(page.getByRole('table')).toBeVisible();
    await expect(page. getByRole('link').filter({ hasText: /^Counter$/ })).toBeVisible();
    await expect(page.getByRole('link').filter({ hasText: /^Weather$/ })).toBeVisible();
    await expect(page.getByRole('link').filter({ hasText: /^Home$/ })).toBeVisible();
  });
});
