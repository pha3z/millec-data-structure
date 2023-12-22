# millec-data-structure
Mapped Index with Linked List of Empty Holes - A high performance fixed index (array) with hole-tracking and fast iteration

## Use MILLEC when:
You want to store items (especially structs) in an indexable data structure (e.g. an array), and you need management of holes (removed items) so you can iterate items and add/remove items. In other words, MILLEC lets you:
- Look up items by integer index, random access O(1)
- Iterate items, skipping holes/removed items in an efficient manner
- Remove items, creating holes O(1)
- Add new items to holes O(1)

## How it works:
MILLEC stores item values in an array; we call it the prime array or Index (the Index part of MILLEC).

MILLEC internally maintains two other sets of a data:
- A bitvector where each bit flags the ACTIVE/INACTIVE (ADDED/REMOVED) status of a corresponding prime array element. This bitvector is the "Map" part of MILLEC.
- A linked list of holes which is woven directly into the prime array. E.g. Each prime array element that is a hole is used to store an integer which is an index to another hole in the array. This is the "Linked List of Empty Holes" part of MILLEC.

NOTE: The linking order of holes is a direct result of removal actions. Each time an item is removed, the created hole links to the last created hole. So if you iterate the list of holes, you will jump around randomly within the array. However, this fact is irrelvant as the purpose of the data structure is *not* to iterate holes. :)



