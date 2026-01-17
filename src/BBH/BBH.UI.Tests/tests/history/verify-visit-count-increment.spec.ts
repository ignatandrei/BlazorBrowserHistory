// spec: test-plan.md
// seed: tests/seed.spec.ts

import { test, expect } from '@playwright/test';

test.describe('History Feature - Basic Functionality', () => {
  test('Verify Visit Count Increments on Repeated Page Visits', async ({ page }) => {
    await page.goto('http://localhost:5023/');

    // Click on Counter link in the navigation menu
    await page.getByRole('navigation').getByRole('link', { name: 'Counter' }).click();

    // Click on Home link in the navigation menu
    await page.getByRole('link', { name: 'Home' }).click();

    // Click on Counter link again
    await page.getByRole('navigation').getByRole('link', { name: 'Counter' }).click();

    // Click on History link to view updated history
    await page.getByRole('link', { name: 'History' }).click();

    // Check the visit count for Counter page
    await expect(page.getByRole('link').filter({ hasText: /^Counter$/ })).toBeVisible();

    // Verify that the Counter page appears with visit count of 2
    const counterLinkRow = page.getByRole('table').getByRole('row');
    console.log(counterLinkRow);
    var res=counterLinkRow.filter({ has: page.getByRole('link', { name: 'Counter' }) }).first();
    await expect(res).toContainText('2');
  });
});
