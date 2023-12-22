# millec-data-structure
Mapped Index with Linked List of Empty Holes - A high performance fixed index (array) with hole-tracking and fast iteration

## Use MILLEC when:
You want to store items (especially structs) in an indexable data structure (e.g. an array), and you need management of holes (removed items) so you can iterate items and add/remove items. In other words, MILLEC lets you:
- Look up items by integer index, random access O(1)
- Iterate items, skipping holes/removed items in an efficient manner
- Remove items, creating holes O(1)
- Add new items to holes O(1)

## How it works:
MILLEC 
