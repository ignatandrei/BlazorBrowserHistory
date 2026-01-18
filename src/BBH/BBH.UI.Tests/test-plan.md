# Blazor Browser History Application Test Plan

## Application Overview


The BBH (Blazor Browser History) application is a Blazor-based web application that tracks user navigation history across pages. The application features a navigation menu with four main pages: Home, Counter, Weather, and History. The History page displays a table showing all visited pages and the count of visits for each page. This test plan covers comprehensive testing of the History tracking functionality, ensuring accurate recording and display of page visits.


## Test Scenarios

### 1. History Feature - Basic Functionality

**Seed:** `tests/seed.spec.ts`

#### 1.1. Verify History Page Displays All Visited Pages with Visit Counts

**File:** `tests/history/verify-history-display.spec.ts`

**Steps:**
  1. Navigate to http://localhost:5023/
  2. Click on Counter link in the navigation menu
  3. Click on Weather link in the navigation menu
  4. Click on Home link in the navigation menu
  5. Click on History link in the navigation menu
  6. Observe the History page table displaying all visited pages

**Expected Results:**
  - History page loads successfully
  - History table displays exactly 4 rows (excluding header)
  - Table columns are: No, Name, Number of visits
  - Counter page is listed with visit count
  - Weather page is listed with visit count
  - Home page is listed with visit count
  - History page itself is listed with visit count
  - Visit counts match actual navigation actions performed

#### 1.2. Verify Visit Count Increments on Repeated Page Visits

**File:** `tests/history/verify-visit-count-increment.spec.ts`

**Steps:**
  1. Navigate to http://localhost:5023/
  2. Click on Counter link in the navigation menu
  3. Click on Home link in the navigation menu
  4. Click on Counter link again
  5. Click on History link to view updated history
  6. Check the visit count for Counter page

**Expected Results:**
  - Counter page appears in the History table
  - Counter page shows visit count of 2
  - Visit count is accurately incremented with each navigation
  - Other pages maintain their original visit counts

#### 1.3. Verify History Item Count Header

**File:** `tests/history/verify-history-count.spec.ts`

**Steps:**
  1. Navigate to http://localhost:5023/
  2. Navigate to Counter page
  3. Navigate to Weather page
  4. Navigate to History page
  5. Observe the 'Number of history items' counter at the top of the page

**Expected Results:**
  - History counter displays correctly
  - Counter shows total number of unique pages visited
  - Counter updates when new pages are visited
  - Counter reflects the number of rows in the history table

#### 1.4. Verify History Page Links Are Clickable

**File:** `tests/history/verify-history-links.spec.ts`

**Steps:**
  1. Navigate to http://localhost:5023/
  2. Click on Counter link
  3. Click on Weather link
  4. Click on History link
  5. Click on the Counter link in the History table
  6. Verify navigation to Counter page succeeds
  7. Navigate back to History page
  8. Click on the Home link in the History table
  9. Verify navigation to Home page succeeds

**Expected Results:**
  - All page links in History table are clickable
  - Clicking a page link navigates to that page
  - Page content updates correctly after clicking history links
  - Navigation maintains history state

### 2. History Feature - Edge Cases and Validation

**Seed:** `tests/seed.spec.ts`

#### 2.1. Verify History with Multiple Visits to Same Page

**File:** `tests/history/verify-multiple-visits.spec.ts`

**Steps:**
  1. Navigate to http://localhost:5023/
  2. Click on Counter link 3 times (with navigation to Home in between each)
  3. Navigate to Weather link
  4. Navigate to Counter link again
  5. Navigate to History page
  6. Observe the Counter visit count

**Expected Results:**
  - Counter page shows accurate visit count
  - Visit count reflects all navigations to Counter page
  - Table shows Counter with correct incremented count
  - History accurately tracks repeated visits

#### 2.2. Verify History Persists During Session

**File:** `tests/history/verify-history-persistence.spec.ts`

**Steps:**
  1. Navigate to http://localhost:5023/
  2. Click on Counter page
  3. Navigate to History page and note the item count
  4. Navigate to Weather page
  5. Navigate to History page again
  6. Verify history includes both previously and newly visited pages

**Expected Results:**
  - History data persists throughout the session
  - New visits are added to existing history
  - Previous history items remain unchanged
  - History grows as new pages are visited

#### 2.3. Verify History Display Order

**File:** `tests/history/verify-display-order.spec.ts`

**Steps:**
  1. Navigate to http://localhost:5023/
  2. Navigate through pages: Counter, Weather, Home, Counter, Weather, History
  3. Navigate back to History page
  4. Observe the order of entries in the History table

**Expected Results:**
  - History table entries are numbered sequentially (1, 2, 3, 4...)
  - Table rows are displayed in a consistent order
  - Most visited or recently visited pages are listed appropriately
  - Entry numbers match the position in the table

#### 2.4. Verify History Page Title and Heading

**File:** `tests/history/verify-page-title.spec.ts`

**Steps:**
  1. Navigate to http://localhost:5023/
  2. Click on History link in navigation
  3. Observe the page title and heading

**Expected Results:**
  - Page title is 'User History'
  - Page displays correct heading
  - Page is clearly identified as the History page
  - Breadcrumb or navigation shows History is the active page

#### 2.5. Verify History with Navigation Menu Links

**File:** `tests/history/verify-menu-navigation.spec.ts`

**Steps:**
  1. Navigate to http://localhost:5023/
  2. Use only navigation menu links to browse: Home, Counter, Weather, History
  3. Navigate to History page
  4. Verify all navigated pages appear in history with correct counts

**Expected Results:**
  - All navigation menu pages are tracked in history
  - Visit counts are accurate for menu-based navigation
  - Navigation through menu properly records page visits
  - History accurately reflects all menu-based browsing

#### 2.6. Verify History Table Header Structure

**File:** `tests/history/verify-table-headers.spec.ts`

**Steps:**
  1. Navigate to http://localhost:5023/
  2. Click on several pages to build history
  3. Navigate to History page
  4. Inspect the history table header row

**Expected Results:**
  - Table has exactly 3 column headers: No, Name, Number of visits
  - Headers are properly aligned with table data
  - All required columns are present
  - Headers are clearly visible and readable

#### 2.7. Verify No Duplicate Entries in History

**File:** `tests/history/verify-no-duplicates.spec.ts`

**Steps:**
  1. Navigate to http://localhost:5023/
  2. Visit Counter page multiple times
  3. Visit Weather page
  4. Visit Counter page again multiple times
  5. Navigate to History page
  6. Check for duplicate entries

**Expected Results:**
  - Counter page appears only once in the history table
  - Each unique page is listed only once
  - Visit count for Counter shows total visits (not multiple rows)
  - No duplicate page entries exist in the table

#### 2.8. Verify History Accuracy After Returning from External Link

**File:** `tests/history/verify-external-link.spec.ts`

**Steps:**
  1. Navigate to http://localhost:5023/
  2. Click on Counter page
  3. Click on History page
  4. Check the 'About' external link is not in history
  5. Verify only internal application pages are tracked

**Expected Results:**
  - External 'About' link does not appear in history table
  - Only internal application pages are tracked
  - External navigation does not affect history counts
  - History only records application page visits

### 3. History Feature - User Interactions

**Seed:** `tests/seed.spec.ts`

#### 3.1. Verify User Can Navigate Using History Page Links

**File:** `tests/history/verify-history-navigation.spec.ts`

**Steps:**
  1. Navigate to http://localhost:5023/ and then to Counter
  2. Navigate to Weather
  3. Navigate to History page
  4. Click on Weather link in the history table
  5. Verify Weather page content is displayed
  6. Click on Counter link in the history table
  7. Verify Counter page content is displayed with correct state

**Expected Results:**
  - Clicking history table links successfully navigates to pages
  - Navigated pages display their full content
  - Page state is maintained when navigating from history
  - User can seamlessly move between pages using history links

#### 3.2. Verify History Updates After Each Navigation

**File:** `tests/history/verify-history-updates.spec.ts`

**Steps:**
  1. Start at Home page
  2. Navigate to Counter and immediately check History
  3. Navigate to Weather and immediately check History
  4. Navigate to Home and immediately check History
  5. Verify each step shows updated history with new entries

**Expected Results:**
  - History updates immediately after each page navigation
  - New pages appear in history table shortly after visiting
  - Visit counts update in real-time
  - No lag between navigation and history display

#### 3.3. Verify History Remains Accessible from All Pages

**File:** `tests/history/verify-history-access.spec.ts`

**Steps:**
  1. Navigate to Home page and click History link
  2. Go back to Counter and click History link
  3. Go back to Weather and click History link
  4. Verify History page is accessible from all pages

**Expected Results:**
  - History page is accessible from Home page
  - History page is accessible from Counter page
  - History page is accessible from Weather page
  - Navigation menu is consistent across all pages

#### 3.4. Verify History Page Responsiveness

**File:** `tests/history/verify-responsiveness.spec.ts`

**Steps:**
  1. Navigate through 5-10 pages to build significant history
  2. Navigate to History page
  3. Observe table rendering and scrolling
  4. Verify page loads without errors or lag

**Expected Results:**
  - History page loads quickly even with multiple entries
  - Table renders correctly with all entries visible
  - Page remains responsive to user interactions
  - No performance degradation with larger history
