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

MILLEC internally maintains two other sets of a data and a few fields:
- A bitvector where each bit flags the ACTIVE/INACTIVE (ADDED/REMOVED) status of a corresponding prime array element. This bitvector is the "Map" part of MILLEC.
- A linked list of holes which is woven directly into the prime array. E.g. Each prime array element that is a hole is used to store an integer which is an index to another hole in the array. This is the "Linked List of Empty Cells" part of MILLEC -- "Empty Cell" made a better acronym than Hole :-).
- An integer field pointing to the most recently created hole (removed item)
- Item Count
- Index of First and Last Items (Non-holes)

NOTE: The linking order of holes is a direct result of removal actions. Each time an item is removed, the created hole links to the last created hole. So if you iterate the list of holes, you will jump around randomly within the array. However, this fact is irrelvant as the purpose of the data structure is *not* to iterate holes. :)

## Add Item
When an item needs to be added, the most recently created hole is where the item gets added. The recent hole field is updated to point to the next most recent hole (by following the hole link).

The corresponding indexed bit is also flipped on.

## Remove Item
When an item is removed, the element value is altered to store index to the most recent hole and the most recent hole field is updated.

The corresponding indexed bit is flipped off.

## Iteration
Exact implementation is prone to change at this point in development.  However the general plan is to leverage the bit array for hyper fast iteration.  SIMD instructions are used to check chunks of bits so that blocks of holes may be skipped. For non-empty blocks, each bit is checked. Non-empty bits correspond to prime array items.

We want users to be able to iterate using:
foreach (ref var ptr in elements) {}

Other ways of iteration may be implemented and tested as well.
