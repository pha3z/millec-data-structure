# millec-data-structure
Mapped Index with Linked List of Empty Cells - A high performance fixed index (array) with empty-cell-tracking (free slots) and fast iteration

## Uses principals described here

https://www.memorymanagement.org/glossary/b.html#term-bitmapped-fit

https://www.memorymanagement.org/glossary/f.html#term-free-block-chain


## New Name Candidates
Since we have revised terminology and begun investigating literature, we should consider alternative data structure names more fitting to the industry standards.  Ideas include:

- milles -- mapped index with linked list of empty slots
- miles -- mapped index with list of empty slots
- bifchain -- bitmapped index with free chain
- bfcarray -- an array using a bitmap and free chain
- bflarray -- an array using a bitmap and free list
- bfclist -- a list using a bitmap and free chain (bitmap free-chain list)
- milfs -- mapped index with list of free slots
- mifc -- pronounced "mif-see" -- mapped index with free chain
- bifc -- pronounced "bif-see" -- bitmapped index with free chain
- biflist -- bitmapped index with free list
- baflist -- bitmapped array and free list

## Use this data structure when:
You want to store items (especially structs) in an indexable data structure (e.g. an array), and you need management of free slots (removed items) so you can iterate items and add/remove items. In other words, this structure lets you:
- Look up items by integer index, random access O(1)
- Iterate items, skipping free slots/removed items in an efficient manner
- Remove items, creating free slots O(1)
- Add new items to free slots O(1)

## How it works:
MILLEC stores item values in an array; we call it the prime array or Index (the Index part of MILLEC).

MILLEC internally maintains two other sets of a data and a few fields:
- A bitvector where each bit flags the ACTIVE/INACTIVE (ADDED/REMOVED) status of a corresponding prime array element. This bitvector is the "Map" part of MILLEC.
- A linked list of free slots which is woven directly into the prime array. E.g. Each prime array element that is a free slot is used to store an integer which is an index to another free slot in the array. This is the "Linked List of Empty Cells" part of MILLEC -- "Empty Cell" made a better acronym than free slot :-).
- An integer field pointing to the most recently created free slot (removed item)
- Item Count
- Index of First and Last Items (Non-free slots)

NOTE: The linking order of free slots is a direct result of removal actions. Each time an item is removed, the created free slot links to the last created free slot. So if you iterate the list of free slots, you will jump around randomly within the array. However, this fact is irrelvant as the purpose of the data structure is *not* to iterate free slots. :)

## Add Item
When an item needs to be added, the most recently created free slot is where the item gets added. The recent free slot field is updated to point to the next most recent free slot (by following the free slot link).

The corresponding bit is also flipped on.

## Remove Item
When an item is removed, the element value is altered to store index to the most recent free slot and the most recent free slot field is updated.

The corresponding bit is flipped off.

## Iteration
Exact implementation is prone to change at this point in development.  However the general plan is to leverage the bit array for hyper fast iteration.  SIMD instructions are used to check chunks of bits so that blocks of free slots may be skipped. For non-empty blocks, each bit is checked. Non-empty bits correspond to prime array items.

We want users to be able to iterate using:
foreach (ref var ptr in elements) {}
foreach (int index in elements) {}
