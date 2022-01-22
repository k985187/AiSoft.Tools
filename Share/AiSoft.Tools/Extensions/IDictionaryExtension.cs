﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using AiSoft.Tools.Systems;

namespace AiSoft.Tools.Extensions
{
    public static class IDictionaryExtension
    {
        /// <summary>
        /// 添加或更新键值对
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="this"></param>
        /// <param name="that">另一个字典集</param>
        /// <returns></returns>
        public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> @this, IDictionary<TKey, TValue> that)
        {
            foreach (var item in that)
            {
                @this[item.Key] = item.Value;
            }
        }

        /// <summary>
        /// 添加或更新键值对
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="this"></param>
        /// <param name="that">另一个字典集</param>
        /// <returns></returns>
        public static void AddOrUpdateTo<TKey, TValue>(this IDictionary<TKey, TValue> @this, IDictionary<TKey, TValue> that)
        {
            foreach (var item in @this)
            {
                that[item.Key] = item.Value;
            }
        }

        /// <summary>
        /// 添加或更新键值对
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="this"></param>
        /// <param name="key">键</param>
        /// <param name="addValue">添加时的值</param>
        /// <param name="updateValueFactory">更新时的操作</param>
        /// <returns></returns>
        public static TValue AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key, TValue addValue, Func<TKey, TValue, TValue> updateValueFactory)
        {
            if (!@this.ContainsKey(key))
            {
                @this.Add(key, addValue);
            }
            else
            {
                @this[key] = updateValueFactory(key, @this[key]);
            }
            return @this[key];
        }

        /// <summary>
        /// 添加或更新键值对
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="this"></param>
        /// <param name="key">键</param>
        /// <param name="addValue">添加时的值</param>
        /// <param name="updateValueFactory">更新时的操作</param>
        /// <returns></returns>
        public static async Task<TValue> AddOrUpdateAsync<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key, TValue addValue, Func<TKey, TValue, Task<TValue>> updateValueFactory)
        {
            if (!@this.ContainsKey(key))
            {
                @this.Add(key, addValue);
            }
            else
            {
                @this[key] = await updateValueFactory(key, @this[key]);
            }
            return @this[key];
        }

        /// <summary>
        /// 添加或更新键值对
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="this"></param>
        /// <param name="key">键</param>
        /// <param name="addValue">添加时的值</param>
        /// <returns></returns>
        public static TValue AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key, TValue addValue, TValue updateValue)
        {
            if (!@this.ContainsKey(key))
            {
                @this.Add(key, addValue);
            }
            else
            {
                @this[key] = updateValue;
            }
            return @this[key];
        }

        /// <summary>
        /// 添加或更新键值对
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="this"></param>
        /// <param name="that">另一个字典集</param>
        /// <param name="updateValueFactory">更新时的操作</param>
        /// <returns></returns>
        public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> @this, IDictionary<TKey, TValue> that, Func<TKey, TValue, TValue> updateValueFactory)
        {
            foreach (var item in that)
            {
                AddOrUpdate(@this, item.Key, item.Value, updateValueFactory);
            }
        }

        /// <summary>
        /// 添加或更新键值对
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="this"></param>
        /// <param name="that">另一个字典集</param>
        /// <param name="updateValueFactory">更新时的操作</param>
        /// <returns></returns>
        public static Task AddOrUpdateAsync<TKey, TValue>(this IDictionary<TKey, TValue> @this, IDictionary<TKey, TValue> that, Func<TKey, TValue, Task<TValue>> updateValueFactory)
        {
            return that.ForeachAsync(item => AddOrUpdateAsync(@this, item.Key, item.Value, updateValueFactory));
        }

        /// <summary>
        /// 添加或更新键值对
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="this"></param>
        /// <param name="that">另一个字典集</param>
        /// <param name="updateValueFactory">更新时的操作</param>
        /// <returns></returns>
        public static void AddOrUpdateTo<TKey, TValue>(this IDictionary<TKey, TValue> @this, IDictionary<TKey, TValue> that, Func<TKey, TValue, TValue> updateValueFactory)
        {
            foreach (var item in @this)
            {
                AddOrUpdate(that, item.Key, item.Value, updateValueFactory);
            }
        }

        /// <summary>
        /// 添加或更新键值对
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="this"></param>
        /// <param name="that">另一个字典集</param>
        /// <param name="updateValueFactory">更新时的操作</param>
        /// <returns></returns>
        public static Task AddOrUpdateAsyncTo<TKey, TValue>(this IDictionary<TKey, TValue> @this, IDictionary<TKey, TValue> that, Func<TKey, TValue, Task<TValue>> updateValueFactory)
        {
            return @this.ForeachAsync(item => AddOrUpdateAsync(that, item.Key, item.Value, updateValueFactory));
        }

        /// <summary>
        /// 添加或更新键值对
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="this"></param> 
        /// <param name="key">键</param>
        /// <param name="addValueFactory">添加时的操作</param>
        /// <param name="updateValueFactory">更新时的操作</param>
        /// <returns></returns>
        public static TValue AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updateValueFactory)
        {
            if (!@this.ContainsKey(key))
            {
                @this.Add(key, addValueFactory(key));
            }
            else
            {
                @this[key] = updateValueFactory(key, @this[key]);
            }
            return @this[key];
        }

        /// <summary>
        /// 添加或更新键值对
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="this"></param>
        /// <param name="key">键</param>
        /// <param name="addValueFactory">添加时的操作</param>
        /// <param name="updateValueFactory">更新时的操作</param>
        /// <returns></returns>
        public static async Task<TValue> AddOrUpdateAsync<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key, Func<TKey, Task<TValue>> addValueFactory, Func<TKey, TValue, Task<TValue>> updateValueFactory)
        {
            if (!@this.ContainsKey(key))
            {
                @this.Add(key, await addValueFactory(key));
            }
            else
            {
                @this[key] = await updateValueFactory(key, @this[key]);
            }
            return @this[key];
        }

        /// <summary> 
        /// 获取或添加
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="this"></param>
        /// <param name="key"></param>
        /// <param name="addValueFactory"></param>
        /// <returns></returns>
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key, Func<TValue> addValueFactory)
        {
            if (!@this.ContainsKey(key))
            {
                @this.Add(key, addValueFactory());
            }
            return @this[key];
        }

        /// <summary>
        /// 获取或添加
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="this"></param>
        /// <param name="key"></param>
        /// <param name="addValueFactory"></param>
        /// <returns></returns>
        public static async Task<TValue> GetOrAddAsync<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key, Func<Task<TValue>> addValueFactory)
        {
            if (!@this.ContainsKey(key))
            {
                @this.Add(key, await addValueFactory());
            }
            return @this[key];
        }

        /// <summary>
        /// 获取或添加
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="this"></param>
        /// <param name="key"></param> 
        /// <param name="addValue"></param>
        /// <returns></returns>
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key, TValue addValue)
        {
            if (!@this.ContainsKey(key))
            {
                @this.Add(key, addValue);
            }
            return @this[key];
        }

        /// <summary>
        /// 遍历IEnumerable
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="action">回调方法</param>
        public static void ForEach<TKey, TValue>(this IDictionary<TKey, TValue> dic, Action<TKey, TValue> action)
        {
            foreach (var item in dic)
            {
                action(item.Key, item.Value);
            }
        }

        /// <summary>
        /// 遍历IDictionary
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="action">回调方法</param>
        public static Task ForEachAsync<TKey, TValue>(this IDictionary<TKey, TValue> dic, Func<TKey, TValue, Task> action)
        {
            return dic.ForeachAsync(x => action(x.Key, x.Value));
        }

        /// <summary> 
        /// 安全的转换成字典集
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector">键选择器</param>
        /// <returns></returns>
        public static NullableDictionary<TKey, TSource> ToDictionarySafety<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var dic = new Dictionary<TKey, TSource>();
            foreach (var item in source)
            {
                dic[keySelector(item)] = item;
            }
            return dic;
        }

        /// <summary>
        /// 安全的转换成字典集
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector">键选择器</param>
        /// <param name="elementSelector">值选择器</param>
        /// <returns></returns>
        public static NullableDictionary<TKey, TElement> ToDictionarySafety<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
        {
            var dic = new NullableDictionary<TKey, TElement>();
            foreach (var item in source)
            {
                dic[keySelector(item)] = elementSelector(item);
            }
            return dic;
        }

        /// <summary>
        /// 安全的转换成字典集
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector">键选择器</param>
        /// <param name="elementSelector">值选择器</param>
        /// <returns></returns>
        public static async Task<NullableConcurrentDictionary<TKey, TElement>> ToDictionarySafetyAsync<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, Task<TElement>> elementSelector)
        {
            var dic = new NullableConcurrentDictionary<TKey, TElement>();
            await source.ForeachAsync(async item => dic[keySelector(item)] = await elementSelector(item));
            return dic;
        }

        /// <summary>
        /// 安全的转换成字典集
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector">键选择器</param>
        /// <returns></returns> 
        public static NullableConcurrentDictionary<TKey, TSource> ToConcurrentDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var dic = new NullableConcurrentDictionary<TKey, TSource>();
            foreach (var item in source)
            {
                dic[keySelector(item)] = item;
            }
            return dic;
        }

        /// <summary>
        /// 安全的转换成字典集
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector">键选择器</param>
        /// <param name="elementSelector">值选择器</param>
        /// <returns></returns>
        public static NullableConcurrentDictionary<TKey, TElement> ToConcurrentDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
        {
            var dic = new NullableConcurrentDictionary<TKey, TElement>();
            foreach (var item in source)
            {
                dic[keySelector(item)] = elementSelector(item);
            }
            return dic;
        }

        /// <summary>
        /// 安全的转换成字典集
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector">键选择器</param>
        /// <param name="elementSelector">值选择器</param>
        /// <returns></returns>
        public static async Task<NullableConcurrentDictionary<TKey, TElement>> ToConcurrentDictionaryAsync<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, Task<TElement>> elementSelector)
        {
            var dic = new ConcurrentDictionary<TKey, TElement>();
            await source.ForeachAsync(async item => dic[keySelector(item)] = await elementSelector(item));
            return dic;
        }

        /// <summary>
        /// 转换成并发字典集合
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static NullableConcurrentDictionary<TKey, TValue> AsConcurrentDictionary<TKey, TValue>(this Dictionary<TKey, TValue> dic) => dic;

        /// <summary>
        /// 转换成普通字典集合
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static NullableDictionary<TKey, TValue> AsDictionary<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dic) => dic;
    }
}