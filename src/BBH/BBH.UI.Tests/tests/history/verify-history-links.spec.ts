// spec: test-plan.md
// seed: tests/seed.spec.ts

import { test, expect } from '@playwright/test';

test.describe('History Feature - Basic Functionality', () => {
  test('Verify History Page Links Are Clickable', async ({ page }) => {
    await page.goto('http://localhost:5023/');

    // Click on Counter link
    await page.getByRole('navigation').getByRole('link', { name: 'Counter' }).click();

    // Click on Weather link
    await page.getByRole('navigation').getByRole('link', { name: 'Weather' }).click();

    // Click on History link
    await page.getByRole('link', { name: 'History' }).click();

    // Click on the Counter link in the History table
    await page.getByRole('link').filter({ hasText: /^Counter$/ }).first().click();

    // Verify navigation to Counter page succeeds
    await expect(page).toHaveTitle('Counter');
    await expect(page.getByRole('heading', { name: 'Counter' })).toBeVisible();

    // Navigate back to History page
    await page.getByRole('link', { name: 'History' }).click();

    // Build up more history by navigating to Home
    await page.getByRole('link', { name: 'Home' }).click();

    // Navigate back to History page
    await page.getByRole('navigation').getByRole('link', { name: 'History' }).click();

    // Click on the Home link in the History table
    await page.getByRole('link').filter({ hasText: /^Home$/ }).click();

    // Verify navigation to Home page succeeds
    await expect(page).toHaveTitle('Home');
    await expect(page.getByText('Hello, world!')).toBeVisible();
  });
});
