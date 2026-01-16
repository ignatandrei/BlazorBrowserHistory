// spec: test-plan.md
// seed: tests/seed.spec.ts
//step1
import { expect,test,LOGIN, BASE, sleep, flash, sleepMessage, setupFirstPage, endTest } from '../utils/common';

import {   Page } from '@playwright/test';

test.describe('History Feature - Basic Functionality', () => {
  test('Verify History Item Count Header', async ({ page }) => {
     
    page= page as Page;
    await page.goto(BASE);

    await sleep(5);
    // Navigate to Counter page
    await (await flash(await page.getByRole('navigation').getByRole('link', { name: 'Counter' }))).click();
    
    await sleep(5);
    // Navigate to Weather page
    await (await flash(await page.getByRole('navigation').getByRole('link', { name: 'Weather' }))).click();

    // Navigate to History page
    await (await flash(page.getByRole('link', { name: 'History' }))).click();

    // Observe the 'Number of history items' counter at the top of the page
    await expect(page.getByText('Number of history items')).toBeVisible();
    await expect(page.getByRole('table')).toBeVisibleFlash();
    

    // Verify the counter reflects the number of rows in the history table
    const historyCountText = await (await flash(await page.getByText('Number of history items'))).textContent();
    expect(historyCountText).toContain('Number of history items 2');
  });
});
